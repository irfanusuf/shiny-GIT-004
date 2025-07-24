import React from 'react'
import { Text, TouchableOpacity, View } from 'react-native'

const Login = ({navigation}) => {
  return (
    <View>

       <Button title='Go back'   onPress={()=>{navigation.navigate("Home")}} />
     




             <Button title='Go to register'   onPress={()=>{navigation.navigate("Register")}} />
        <TouchableOpacity onPress={()=>{}} >   <Text>  Login </Text>  </TouchableOpacity>


    </View>
  )
}

export default Login