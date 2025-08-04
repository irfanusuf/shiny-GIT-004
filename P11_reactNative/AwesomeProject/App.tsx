
import { StatusBar, StyleSheet, Text, useColorScheme, View } from 'react-native';
import HomeScreen from './screens/HomeScreen';
import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { NavigationContainer } from '@react-navigation/native';
import ProfileScreen from './screens/ProfileScreen';
import RegisterScreen from './screens/RegisterScreen';
import LoginScreen from './screens/LoginScreen';
import ProductsScreen from './screens/ProductsScreen';
import ProductDetails from './screens/ProductDetails';
import CameraCapture from './screens/Camera';




export type ParamList = {
  Home: undefined;
  Profile: undefined;
  Login: undefined;
  Register: undefined;
  Products: undefined;
  ProductDetails: undefined
  Camera : undefined
};

const Stack = createNativeStackNavigator<ParamList>();

function App() {
  const isDarkMode = useColorScheme() === 'dark';


  return (
    <NavigationContainer>
      <StatusBar barStyle={isDarkMode ? 'light-content' : 'dark-content'} />
      <Stack.Navigator initialRouteName="Home">
        <Stack.Screen name="Home" component={HomeScreen} />
        <Stack.Screen name="Profile" component={ProfileScreen} />
        <Stack.Screen name="Register" component={RegisterScreen} />
        <Stack.Screen name="Login" component={LoginScreen} />
        <Stack.Screen name="Products" component={ProductsScreen} />
        <Stack.Screen name="ProductDetails" component={ProductDetails} />
        <Stack.Screen name="Camera" component={CameraCapture} />
      </Stack.Navigator>
    </NavigationContainer>

  );
}



export default App;
