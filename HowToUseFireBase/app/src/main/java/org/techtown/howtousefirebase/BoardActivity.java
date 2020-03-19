package org.techtown.howtousefirebase;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.graphics.drawable.Drawable;
import android.media.Image;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.bumptech.glide.Glide;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.MutableData;
import com.google.firebase.database.Transaction;
import com.google.firebase.database.ValueEventListener;
import com.google.firebase.storage.FirebaseStorage;

import java.util.ArrayList;
import java.util.List;

public class BoardActivity extends AppCompatActivity {
    private RecyclerView recyclerView;
    private List<ImageObject> list=new ArrayList<>();
    private List<String> keylist=new ArrayList<>();
    private FirebaseDatabase database;
    private FirebaseAuth auth;
    private FirebaseStorage storage;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_board);
        database=FirebaseDatabase.getInstance();
        auth=FirebaseAuth.getInstance();
        storage=FirebaseStorage.getInstance();

        recyclerView=findViewById(R.id.recycler);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        final BoardRecyclerViewAdapter boardRecyclerViewAdapter=new BoardRecyclerViewAdapter();
        recyclerView.setAdapter(boardRecyclerViewAdapter);

        database.getReference().child("images").addValueEventListener(new ValueEventListener() { //현재 위치는 image의 하위트리
            @Override
            public void onDataChange(DataSnapshot dataSnapshot) {
                list.clear();
                keylist.clear();
                for (DataSnapshot data: dataSnapshot.getChildren()) { //2개가 저장되있으면 2번 반복
                    ImageObject object = data.getValue(ImageObject.class); //JSON -> Object (자식)
                    keylist.add(object.title); //해당 이미지의 제목이 곧 그 이미지가 들어갈 폴더이다.
                    list.add(object);
               }
                boardRecyclerViewAdapter.notifyDataSetChanged();
            }

            @Override
            public void onCancelled(DatabaseError databaseError) {
                //DoSomething if cancelled
            }
        });

    }
    class BoardRecyclerViewAdapter extends RecyclerView.Adapter<BoardRecyclerViewAdapter.ItemHolder>{
        @NonNull
        @Override
        public ItemHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
            View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_board,parent,false);
            return new ItemHolder(view);
        }

        @Override
        public void onBindViewHolder(@NonNull ItemHolder holder, final int position) {
            final ItemHolder itemHolder=holder;

            if(list.get(position).stars.containsKey(auth.getCurrentUser().getUid())){ //좋아요가 눌렸니?
                itemHolder.starImage.setImageResource(R.drawable.sharp_star_black_24); //꽉찬 별
            }
            else{
                itemHolder.starImage.setImageResource(R.drawable.sharp_star_border_black_24); //빈 별
            }

            holder.textView1.setText(list.get(position).title);
            holder.textView2.setText(list.get(position).contents);

            //해당 아이템의 뷰의 이미지뷰안에 해당 주소의 이미지를 로드한다.
            Glide.with(holder.itemView.getContext()).load(list.get(position).imageUri).into(holder.imageView);

            holder.starImage.setOnClickListener(new View.OnClickListener() { //스타버튼 클릭 이벤트
                @Override
                public void onClick(View v) {

                    //내가 좋아요를 누른 이미지의 데이터베이스참조로 접근
                    onStarClicked(database.getReference().child("images").child(keylist.get(position)));
                    if(list.get(position).stars.containsKey(auth.getCurrentUser().getUid())){ //좋아요를 눌렀니?
                        itemHolder.starImage.setImageResource(R.drawable.sharp_star_black_24); //꽉찬 별
                    }
                    else{
                        itemHolder.starImage.setImageResource(R.drawable.sharp_star_border_black_24); //빈 별
                    }
                }
            });

            holder.delbtn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) { //삭제버튼 클릭 이벤트
                    deleteContents(position);
                }
            });
        }

        @Override
        public int getItemCount() {
            return list.size();
        }

        private void onStarClicked(DatabaseReference postRef) {
            postRef.runTransaction(new Transaction.Handler() {
                @Override
                public Transaction.Result doTransaction(MutableData mutableData) {
                    //트랜젝션 실행

                    ImageObject p = mutableData.getValue(ImageObject.class); //JSON -> Object
                    if (p == null) { //없다면,
                        return Transaction.success(mutableData);
                    }

                    if (p.stars.containsKey(auth.getCurrentUser().getUid())) { //이 유저가 이미 좋아요를 눌렀는가?
                        // 해당 이미지의 좋아요 갯수 -1
                        p.starCount = p.starCount - 1;

                        //해당 유저는 좋아요를 눌렀었으므로 삭제
                        p.stars.remove(auth.getCurrentUser().getUid());
                    } else {

                        // 해당 이미지의 좋아요 갯수 +1
                        p.starCount = p.starCount + 1;

                        //해당 유저의 id와 좋아요를 눌렀다는 증거의 true를 Map으로 저장
                        p.stars.put(auth.getCurrentUser().getUid(), true);
                    }

                    // 바뀐 데이터 업데이트
                    mutableData.setValue(p);
                    return Transaction.success(mutableData);
                }

                @Override
                public void onComplete(DatabaseError databaseError, boolean b,
                                       DataSnapshot dataSnapshot) {
                    // Do Something if Transaction completed
                }
            });
        }

        public void deleteContents(int pos){
            OnSuccessListener onSuccessListener=new OnSuccessListener() {
                @Override
                public void onSuccess(Object o) {
                    Toast.makeText(BoardActivity.this,"삭제 완료",Toast.LENGTH_SHORT).show();
                }
            };
            OnFailureListener onFailureListener=new OnFailureListener() {
                @Override
                public void onFailure(@NonNull Exception e) {
                    Toast.makeText(BoardActivity.this,"삭제 실패",Toast.LENGTH_SHORT).show();
                }
            };

            storage.getReference().child("images").child(list.get(pos).imageName).delete();

            database.getReference().child("images").child(keylist.get(pos)).removeValue()
                    .addOnSuccessListener(onSuccessListener)
                    .addOnFailureListener(onFailureListener);
        }

        public class ItemHolder extends RecyclerView.ViewHolder {
            ImageView imageView,starImage;
            TextView textView1,textView2;
            Button delbtn;
            public ItemHolder(View view) {
                super(view);
                imageView=view.findViewById(R.id.myimage);
                textView1=view.findViewById(R.id.info1);
                textView2=view.findViewById(R.id.info2);
                starImage=view.findViewById(R.id.star);
                delbtn=view.findViewById(R.id.delbtn);
            }
        }
    }
}
