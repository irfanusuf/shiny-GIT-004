import { NativeStackScreenProps } from "@react-navigation/native-stack";
import { Button } from "react-native";

import { ParamList } from "../App";
import { Text } from "react-native";
import { View } from "react-native";




type Props = NativeStackScreenProps<ParamList , "ProductDetails">


export default function ProductDetails ({navigation} : Props){


    return (
        <View> 

            <Text> Producct Details</Text>



        </View>
    )
}