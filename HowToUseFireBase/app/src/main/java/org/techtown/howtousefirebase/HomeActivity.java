package org.techtown.howtousefirebase;

import android.Manifest;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.ContentResolver;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.loader.content.CursorLoader;

import com.google.android.gms.tasks.Continuation;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.remoteconfig.FirebaseRemoteConfig;
import com.google.firebase.remoteconfig.FirebaseRemoteConfigSettings;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.StorageReference;
import com.google.firebase.storage.UploadTask;

import java.io.File;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;

public class HomeActivity extends AppCompatActivity {
    private LinearLayout layout;
    private List<String> list=new ArrayList<>();
    private ListView listView;
    private FirebaseAuth auth;
    private FirebaseStorage storage;
    private FirebaseDatabase database;
    private EditText title,cont;
    private Button upload;
    private ImageView imageView;
    private String imagePath=null;
    private FirebaseRemoteConfig mFirebaseRemoteConfig;
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);
        initList();
        layout=findViewById(R.id.back);
        listView=findViewById(R.id.list);
        title=findViewById(R.id.title);
        cont=findViewById(R.id.content);
        upload=findViewById(R.id.upload);
        imageView=findViewById(R.id.image);
        auth=FirebaseAuth.getInstance();
        storage=FirebaseStorage.getInstance();
        database=FirebaseDatabase.getInstance();

        if(Build.VERSION.SDK_INT>=Build.VERSION_CODES.M){
            requestPermissions(new String[]{Manifest.permission.READ_EXTERNAL_STORAGE,Manifest.permission.WRITE_EXTERNAL_STORAGE},0);
        }

        ArrayAdapter<String> adapter=new ArrayAdapter<>(this,android.R.layout.simple_list_item_1,list);
        listView.setAdapter(adapter);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                switch(position){
                    case 0: //갤러리
                        Intent intent=new Intent();
                        intent.setType(MediaStore.Images.Media.CONTENT_TYPE);
                        intent.setAction(Intent.ACTION_PICK);
                        startActivityForResult(intent,101);
                        break;
                    case 1: //보드
                        startActivity(new Intent(HomeActivity.this,BoardActivity.class));
                        break;
                    case 2: //로그아웃
                        auth.signOut();
                        finish();
                        Intent intent1=new Intent(HomeActivity.this,MainActivity.class);
                        startActivity(intent1);
                        break;
                }
            }
        });
        upload.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                upLoad(imagePath);
            }
        });
        remoteConfig();
    }
    private void remoteConfig(){
        mFirebaseRemoteConfig = FirebaseRemoteConfig.getInstance();

        //RemoteConfig의 세팅을 설정(디버깅 테스트를 할 때 사용한다고함)
        //개발과정 테스트시, 엡을 자주 껏다 키기때문에, 호출이 제한될 수 있기때문에, 0초로 설정하여 제한을 없앰
        FirebaseRemoteConfigSettings configSettings = new FirebaseRemoteConfigSettings.Builder()
                .setMinimumFetchIntervalInSeconds(0)
                .build();

        mFirebaseRemoteConfig.setConfigSettingsAsync(configSettings); //세팅값 설정

        //서버에 매칭되는 값이 없을때 참조(기본값)
        mFirebaseRemoteConfig.setDefaults(R.xml.remote_config_defaults);

        mFirebaseRemoteConfig.fetchAndActivate() //fetch!
                .addOnCompleteListener(this, new OnCompleteListener<Boolean>() {
                    @Override
                    public void onComplete(@NonNull Task<Boolean> task) {
                        if (task.isSuccessful()) { //fetch가 성공했을때,
                            boolean updated = task.getResult();
                            //DoSomething

                        } else { //fetch가 실패했을때,
                            Toast.makeText(HomeActivity.this, "Fetch failed", Toast.LENGTH_SHORT).show();
                        }
                        displayWelcomeMessage();
                    }
                });
    }

    private void displayWelcomeMessage() {
        String backColor=mFirebaseRemoteConfig.getString("backColor");
        Boolean aBoolean=mFirebaseRemoteConfig.getBoolean("welcome_message_caps");
        String welcome_message=mFirebaseRemoteConfig.getString("welcome_message");

        layout.setBackgroundColor(Color.parseColor(backColor));
        if(aBoolean){
            AlertDialog.Builder builder=new AlertDialog.Builder(this);
            builder.setMessage(welcome_message).setPositiveButton("OK", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    HomeActivity.this.finish();
                }
            });
            builder.create().show();
        }
    }


    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
       super.onActivityResult(requestCode, resultCode, data);
       if(requestCode==101 && resultCode==RESULT_OK){
           imagePath=getPath(data.getData());
           File file = new File(imagePath);
           imageView.setImageURI(Uri.fromFile(file));
       }
    }
    public String getPath(Uri uri){
        String[] proj={MediaStore.Images.Media.DATA};
        CursorLoader cursorLoader=new CursorLoader(this,uri,proj,null,null,null);
        Cursor cursor=cursorLoader.loadInBackground();
        int index=cursor.getColumnIndexOrThrow(MediaStore.Images.Media.DATA);

        cursor.moveToFirst();

        return cursor.getString(index);
    }
    public void upLoad(String path){
        StorageReference reference=storage.getReferenceFromUrl("gs://myproject-bc9a3.appspot.com"); //스토리지 버킷주소 참조

        final Uri file=Uri.fromFile(new File(path)); //해당 파일의 경로
        final StorageReference putref=reference.child("images/"+file.getLastPathSegment()); //기존 reference의 참조주소에서 해당 하위트리를 침조
        UploadTask task=putref.putFile(file); //해당 경로에 파일을 삽입

        Task<Uri> urlTask = task.continueWithTask(new Continuation<UploadTask.TaskSnapshot, Task<Uri>>() {
            @Override
            public Task<Uri> then(@NonNull Task<UploadTask.TaskSnapshot> task) throws Exception {
                if (!task.isSuccessful()) {
                    throw task.getException();
                }
                // Continue with the task to get the download URL
                return putref.getDownloadUrl();
            }
        }).addOnCompleteListener(new OnCompleteListener<Uri>() {
            @Override
            public void onComplete(@NonNull Task<Uri> task) {
                if (task.isSuccessful()) {
                    Toast.makeText(HomeActivity.this,"파일 업로드 성공",Toast.LENGTH_SHORT).show();

                    ImageObject object=new ImageObject(); //하나의 파일 정보가 될것
                    object.imageUri=task.getResult().toString(); //파일 이미지 경로
                    object.title=title.getText().toString();
                    object.contents=cont.getText().toString();
                    object.uid=auth.getCurrentUser().getUid();
                    object.userId=auth.getCurrentUser().getEmail();
                    object.imageName=file.getLastPathSegment();

                    database.getReference().child("images").child(object.title).setValue(object); //데이터베이스 참조 후, images 하위트리로 가서 값을 저장
                } else {
                    // Handle failures
                    // ...
                }
            }
        });
    }

    public void initList(){
        list.add("Gallery");
        list.add("Board");
        list.add("logout");
    }

    @Override
    protected void onDestroy() {
        auth.signOut();
        super.onDestroy();
    }
}
