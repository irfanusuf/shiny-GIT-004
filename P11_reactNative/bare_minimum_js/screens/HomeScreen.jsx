import React from 'react'
import { Button, Text, View } from 'react-native'

const HomeScreen = ({navigation}) => {
    return (
        <View>

          




            <Text>
                HomeScreen
            </Text>


              <Button title='Go to Details' onPress={()=>{navigation.navigate("Details")}} />

        </View>
    )
}

export default HomeScreen