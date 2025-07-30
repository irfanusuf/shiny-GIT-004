npm install react-native-vision-camera \
@react-native-async-storage/async-storage \
@react-native-clipboard/clipboard \
@react-native-community/netinfo \
@react-native-masked-view/masked-view \
@react-navigation/native \
@react-navigation/native-stack \
axios \
react \
react-native \
react-native-biometrics \
react-native-device-info \
react-native-encrypted-storage \
react-native-fs \
react-native-geolocation-service \
react-native-gesture-handler \
react-native-haptic-feedback \
react-native-keychain \
react-native-localize \
react-native-permissions \
react-native-push-notification \
react-native-qrcode-scanner \
react-native-reanimated \
react-native-safe-area-context \
react-native-screens \
react-native-share \
react-native-svg \
react-native-touch-id \
react-native-vector-icons \
react-native-worklets\






permissions for opening and camera


<uses-permission android:name="android.permission.CAMERA" />
<uses-permission android:name="android.permission.RECORD_AUDIO" />



import { Camera } from 'react-native-vision-camera';

useEffect(() => {
(async () => {
const status = await Camera.requestCameraPermission();
console.log(status); // 'authorized' or 'denied'
})();
}, []);


increase ram heap size
org.gradle.jvmargs=-Xmx6144m -XX:MaxMetaspaceSize=2048m -Dfile.encoding=UTF-8

android.enableJetifier=true

guide for connecting on wifi and installing app using usb

adb tcpip 5555 // when phone is connected to the usb

adb shell ip route // get phone ip

adb connect 192.168.29.224:5555 connect to the ip which u got from above command

Shake your device (or use adb shell input keyevent 82)

ip addr | grep inet or hostname -I

use these ccommannds to get your system ip and then go to the mobile app u installed using usb
and put into the ip and port number on which the dev server is working like 8081
