import React from 'react';
import { View, Text, Button } from 'react-native';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { RootStackParamList } from '../App';



type Props = NativeStackScreenProps<RootStackParamList, 'Profile'>;




export default function ProfileScreen({ navigation } : Props) {
  return (
    <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
      <Text>Profile Screen</Text>
      <Button 
        title="Go back"
        onPress={() => navigation.goBack()}
      />
    </View>
  );
}
