import React from 'react'
import { Button, Text, View } from 'react-native'

const Details = ({navigation}) => {
  return (
    <View>

        <Button title='Go back'   onPress={()=>{navigation.navigate("Home")}} />

        <Text>
            details page
        </Text>
    </View>
  )
}

export default Details