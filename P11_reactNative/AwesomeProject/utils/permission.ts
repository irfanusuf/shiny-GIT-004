import { PermissionsAndroid, Platform } from 'react-native';

async function requestCameraPermission() {
  if (Platform.OS === 'android') {
    try {
      const granted = await PermissionsAndroid.requestMultiple([
        PermissionsAndroid.PERMISSIONS.CAMERA,
        PermissionsAndroid.PERMISSIONS.RECORD_AUDIO
      ]);
      
      const cameraGranted = granted[PermissionsAndroid.PERMISSIONS.CAMERA] === PermissionsAndroid.RESULTS.GRANTED;
      const micGranted = granted[PermissionsAndroid.PERMISSIONS.RECORD_AUDIO] === PermissionsAndroid.RESULTS.GRANTED;

      if (cameraGranted && micGranted) {
        console.log('Camera & Audio permissions granted');
      } else {
        console.warn('Permissions denied');
      }
    } catch (err) {
      console.warn(err);
    }
  }
}



