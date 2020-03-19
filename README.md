# Supplementary_information  
## Crash reporting  
사용자가 앱을 사용시, 비정상정인 오류가 발생했을때 관리자가 이 오류를 쉽게 파악할 수 있다.  
<a href=https://firebase.google.com/docs/crashlytics>How to use Crashlytics</a>  
  
## RemoteConfig  
원격으로 앱의 특정부분을 조절할 수 잇다.  
<a href=https://firebase.google.com/docs/remote-config/android>How to use RemoteConfig</a>  
  
<b>res/xml/remote_config_defaults.xml</b>  
```
<?xml version="1.0" encoding="utf-8"?>
<!-- START xml_defaults -->
<defaultsMap>
    <entry>
        <key>backColor</key>
        <value>#BEF0F7</value>
    </entry>
    <entry>
        <key>welcome_message_caps</key>
        <value>false</value>
    </entry>
    <entry>
        <key>welcome_message</key>
        <value>Welcome to my awesome app!</value>
    </entry>
</defaultsMap>
    <!-- END xml_defaults -->
```
  
<b>HomeActivity 의 remoteConfig 설정 </b>  
우선, 다음과 같은 객체를 선언한다.  
```
private FirebaseRemoteConfig mFirebaseRemoteConfig;
```
  
그리고, 다음 메소드를 추가한다.  
```
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
```  
  
그리고, 적절한 곳에 remoteConfig()를 실행하게끔 한다.  
  
## AdMob  
어렵지 않으니, 다음을 참조하여 적절히 사용하자.  
<a href=https://firebase.google.com/docs/admob/android/quick-start>How to use AdMob</a>
