import React, { useEffect, useRef, useState } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, Alert } from 'react-native';
import { Camera, useCameraDevices } from 'react-native-vision-camera';

const CameraCapture = () => {
  const [hasPermission, setHasPermission] = useState(false);
  const camera = useRef<Camera>(null);
  const devices = useCameraDevices();
  const device = devices.back;

  // Request permission function
  const requestPermissions = async () => {
    const cameraPermission = await Camera.requestCameraPermission();
    const micPermission = await Camera.requestMicrophonePermission();
    setHasPermission(cameraPermission === 'authorized' && micPermission === 'authorized');
  };

  useEffect(() => {
    requestPermissions();
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
        <TouchableOpacity style={styles.retryButton} onPress={requestPermissions}>
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
  buttonText: {
    fontSize: 28,
  },
});
