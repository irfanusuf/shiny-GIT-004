import React from 'react';
import { View, Text, Button } from 'react-native';

import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { RootStackParamList } from '../App';



type Props = NativeStackScreenProps<RootStackParamList, 'Home'>;


export default function HomeScreen({ navigation }: Props ) {
  return (
    <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
      <Text>Home Screen</Text>
      <Button 
        title="Go to Profile"
        onPress={() => navigation.navigate('Profile')}
      />


         <Button 
        title="Go to Login"
        onPress={() => navigation.navigate('Login')}
      />
    </View>
  );
}
