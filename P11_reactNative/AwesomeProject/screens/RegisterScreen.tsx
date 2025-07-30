import { NativeStackScreenProps } from "@react-navigation/native-stack";
import { Button, View } from "react-native";
import { ParamList } from "../App";




type Props = NativeStackScreenProps<ParamList , "Register">


export default function RegisterScreen ({navigation} : Props){


return (

    <View> 
    
        <Button title="Go to login " onPress={()=>{navigation.navigate("Login")}}/>
    </View>
)

}