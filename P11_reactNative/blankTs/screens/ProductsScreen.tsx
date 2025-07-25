import { NativeStackScreenProps } from "@react-navigation/native-stack";
import { Button } from "react-native";

import { ParamList } from "../App";
import { View } from "react-native";




type Props = NativeStackScreenProps<ParamList , "Products">


export default function ProductsScreen ({navigation} : Props){


    return (
        <View> 
            <Button title="Details" onPress={()=>{navigation.navigate("ProductDetails")}}/> 
        </View>
    )
}