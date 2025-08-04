import React, { useEffect, useRef, useState } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, Alert, PermissionsAndroid, Platform } from 'react-native';
import { Camera, useCameraDevices } from 'react-native-vision-camera';



async function requestCameraPermission() {
  if (Platform.OS === 'android') {
    try {
      const granted = await PermissionsAndroid.requestMultiple([
        PermissionsAndroid.PERMISSIONS.CAMERA,
        PermissionsAndroid.PERMISSIONS.RECORD_AUDIO,
      ]);
      
      const cameraGranted = granted[PermissionsAndroid.PERMISSIONS.CAMERA] === PermissionsAndroid.RESULTS.GRANTED;
      const micGranted = granted[PermissionsAndroid.PERMISSIONS.RECORD_AUDIO] === PermissionsAndroid.RESULTS.GRANTED;

      if (cameraGranted && micGranted) {
        console.log('Camera & Audio permissions granted');
        return true;
      } else {
        console.warn('Permissions denied');
        return false;
      }
    } catch (err) {
      console.warn(err);
      return false;
    }
  }
  // iOS permissions handled by react-native-vision-camera internally
  return true;
}

const CameraCapture = () => {


  const [hasPermission, setHasPermission] = useState(false);
  const [cameraMode , setCameraMode] = useState("back")

  const camera = useRef<Camera>(null);

  const devices = useCameraDevices();

  const device = devices.find((d) => d.position === cameraMode);

  const checkPermissions = async () => {
    // Use your custom permission function here
    const granted = await requestCameraPermission();
    setHasPermission(true);
  };

  useEffect(() => {
    checkPermissions();
  }, []);

  const takePhoto = async () => {
    if (camera.current == null) return;
    try {
      const photo = await camera.current.takePhoto({
        flash: 'off',
      });
      Alert.alert('Photo taken!', JSON.stringify(photo, null, 2));
    } catch (err) {
      console.error('Failed to take photo:', err);
      Alert.alert('Error', 'Failed to take photo.');
    }
  };

  if (device == null) {
    return <Text style={styles.loadingText}>Loading camera...</Text>;
  }

  if (!hasPermission) {
    return (
      <View style={styles.permissionContainer}>
        <Text style={styles.loadingText}>Camera permission not granted</Text>
        <TouchableOpacity style={styles.retryButton} onPress={checkPermissions}>
          <Text style={styles.retryText}>Grant Permission</Text>
        </TouchableOpacity>
      </View>
    );
  }

  return (



    <View style={styles.container}>

      <Camera
        ref={camera}
        style={StyleSheet.absoluteFill}
        device={device}
        isActive={true}
        photo={true}
      />


      <TouchableOpacity style={styles.captureButton} onPress={takePhoto}>
        <Text style={styles.buttonText}>📷</Text>
      </TouchableOpacity>

{/* 
      <TouchableOpacity style={styles.flipButton} onPress={()=>{setCameraMode("front")}}>
        <Text style={styles.buttonText}>Flip</Text>
      </TouchableOpacity> */}

    </View>



  );
};

export default CameraCapture;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'black',
  },
  loadingText: {
    flex: 1,
    color: 'white',
    textAlign: 'center',
    textAlignVertical: 'center',
    fontSize: 18,
  },
  permissionContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  retryButton: {
    marginTop: 20,
    backgroundColor: '#1e90ff',
    padding: 15,
    borderRadius: 8,
  },
  retryText: {
    color: 'white',
    fontWeight: 'bold',
  },
  captureButton: {
    position: 'absolute',
    bottom: 40,
    alignSelf: 'center',
    backgroundColor: '#ffffff90',
    padding: 20,
    borderRadius: 40,
  },

  flipButton :{
    position: 'absolute',
    bottom: 40,
    alignSelf : "flex-start",
    backgroundColor: '#ffffff90',
    padding: 20,
    borderRadius: 40,

  },
  buttonText: {
    fontSize: 28,
  },
});
