package org.techtown.howtousefirebase;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdView;
import com.google.android.gms.ads.MobileAds;
import com.google.android.gms.auth.api.signin.GoogleSignIn;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInClient;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.common.SignInButton;
import com.google.android.gms.common.api.ApiException;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthCredential;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.auth.GoogleAuthProvider;

public class MainActivity extends AppCompatActivity {
    private EditText email,pass; //email, password 에딧텍스트
    private Button sign; // 로그인 및 회원가입 버튼
    private SignInButton button;
    //private Button logout;
    private GoogleSignInClient mGoogleSignInClient;
    private int RC_SIGN_IN = 10;
    private FirebaseAuth mAuth;
    private FirebaseAuth.AuthStateListener authStateListener; //로그인 상태뱐화에 따른 리스너
    private AdView adView;

    @Override
    public void onStart() {
        super.onStart();
        FirebaseUser currentUser = mAuth.getCurrentUser();
        mAuth.addAuthStateListener(authStateListener);

        /*if(currentUser!=null)
            Toast.makeText(MainActivity.this,currentUser.getEmail(),Toast.LENGTH_SHORT).show();*/

    }

    @Override
    protected void onStop() {
        super.onStop();
        if(authStateListener!=null)
            mAuth.removeAuthStateListener(authStateListener);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        button=findViewById(R.id.loginbtn);
        //logout=findViewById(R.id.logout);
        email=findViewById(R.id.emailtext);
        pass=findViewById(R.id.passwordtext);
        sign=findViewById(R.id.signup);
        adView=findViewById(R.id.adView);

        GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
                .requestIdToken(getString(R.string.default_web_client_id))
                .requestEmail()
                .build(); //구글 인증을 할때의 옵션 설정

        mGoogleSignInClient = GoogleSignIn.getClient(this, gso); //구글 인증을 위한 객체생성
        mAuth = FirebaseAuth.getInstance(); //파이어베이스 인증을 위한 객체생성

        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                signIn();
            }
        });
        /*logout.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                signOut();
            }
        });*/
        sign.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                signInUser(email.getText().toString(),pass.getText().toString());
            }
        });
        authStateListener=new FirebaseAuth.AuthStateListener() {
            @Override
            public void onAuthStateChanged(@NonNull FirebaseAuth firebaseAuth) {
                FirebaseUser user=firebaseAuth.getCurrentUser();
                if(user!=null){
                    Toast.makeText(MainActivity.this,"로그인된 상태",Toast.LENGTH_SHORT).show();
                }
                else{
                    Toast.makeText(MainActivity.this,"로그아웃된 상태",Toast.LENGTH_SHORT).show();
                }
            }
        };
        MobileAds.initialize(this, "ca-app-pub-3940256099942544~3347511713");
        AdRequest adRequest=new AdRequest.Builder().build();
        adView.loadAd(adRequest);
    }
    public void createNewAccount(String email,String password){ //회원가입 메소드
        mAuth.createUserWithEmailAndPassword(email, password)
                .addOnCompleteListener(this, new OnCompleteListener<AuthResult>() {
                    @Override
                    public void onComplete(@NonNull Task<AuthResult> task) {
                        if (task.isSuccessful()) { // 인증이 성공시,
                            FirebaseUser user = mAuth.getCurrentUser(); //인증받은 user의 객체
                            Intent intent=new Intent(MainActivity.this,HomeActivity.class);
                            startActivity(intent);
                            finish();

                            //Toast.makeText(MainActivity.this,"회원가입 성공",Toast.LENGTH_SHORT).show(); 리스너를 설정했기때문에 필요없음
                            //DoSomething(user);
                        }
                        else {// 인증이 실패시,
                           //Something else...
                        }
                    }
                });
    }
    public void signInUser(String email,String password){//기존 사용자의 로그인 메소드
        final String Email=email;
        final String Password=password;
        mAuth.signInWithEmailAndPassword(email, password)
                .addOnCompleteListener(this, new OnCompleteListener<AuthResult>() {
                    @Override
                    public void onComplete(@NonNull Task<AuthResult> task) {
                        if (task.isSuccessful()) { //인증 성공시,
                            FirebaseUser user = mAuth.getCurrentUser();
                            Intent intent=new Intent(MainActivity.this,HomeActivity.class);
                            startActivity(intent);
                            finish();

                            //Toast.makeText(MainActivity.this,user.getEmail(),Toast.LENGTH_SHORT).show();
                            //DoSomething(user);
                        }
                        else { //인증 실패시,
                            createNewAccount(Email,Password); //기존 로그인 사용자가 아니라면 회원가입을 한다.
                        }
                    }
                });
    }
    private void signIn() { //google plus에 요청 : 이 사람이 구글 사용자니?
        Intent signInIntent = mGoogleSignInClient.getSignInIntent(); //인증할수있는곳 intent
        startActivityForResult(signInIntent, RC_SIGN_IN);
    }
    /*private void signOut() {
        // 파이어베이스 로그아웃
        mAuth.signOut();
        //Toast.makeText(MainActivity.this,"로그아웃 완료",Toast.LENGTH_SHORT).show();

        // 구글 로그아웃
        mGoogleSignInClient.signOut().addOnCompleteListener(this,
                new OnCompleteListener<Void>() {
                    @Override
                    public void onComplete(@NonNull Task<Void> task) {
                        //Toast.makeText(MainActivity.this,"로그아웃 완료",Toast.LENGTH_SHORT).show();
                    }
                }); //로그아웃에 대한 리스너 설정
    }*/


    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (requestCode == RC_SIGN_IN) { //응 구글 사용자 맞아!
            Task<GoogleSignInAccount> task = GoogleSignIn.getSignedInAccountFromIntent(data);
            try {
                // 구글인증이 성공시, 파이어베이스로 인증
                GoogleSignInAccount account = task.getResult(ApiException.class);
                firebaseAuthWithGoogle(account); //그럼 파이어베이스 인증으로 넘길께!
            } catch (ApiException e) {
                // 구글인증 실패
                Log.w("TAG", "Google sign in failed", e);
            }
        }
    }
    private void firebaseAuthWithGoogle(GoogleSignInAccount acct) {

        AuthCredential credential = GoogleAuthProvider.getCredential(acct.getIdToken(), null); //그 사람에 대한 정보를 받고,
        mAuth.signInWithCredential(credential)
                .addOnCompleteListener(this, new OnCompleteListener<AuthResult>() { //해당 인증에대한 리스너 생성
                    @Override
                    public void onComplete(@NonNull Task<AuthResult> task) {
                        if (task.isSuccessful()) { //파이어베이스 인증이 성공
                            FirebaseUser user = mAuth.getCurrentUser(); //해당 유저객체
                            Intent intent=new Intent(MainActivity.this,HomeActivity.class);
                            startActivity(intent);
                            finish();

                            //Toast.makeText(MainActivity.this,user.getEmail(),Toast.LENGTH_SHORT).show(); //사용자 이메일을 토스트 메세지로 받고있음

                            //DoSomething(user)

                        } else { //인증실패
                            //Something else...
                        }
                    }
                });
    }
}
