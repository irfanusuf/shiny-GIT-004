import { NativeStackScreenProps } from "@react-navigation/native-stack";

import React, { useState } from "react";
import {
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from "react-native";
import { ParamList } from "../App";

import Icon from 'react-native-vector-icons/FontAwesome';



type Props = NativeStackScreenProps<ParamList, "Login">;

export default function LoginScreen({navigation}:Props) {


  const [loading, setLoading] = useState(false);


  const [formData, setFormData] = useState({
    username: "irfanusuf33@gmail.com",
    password: "12345678"
  })

  const handleLogin = async () => {
    try {

      if (formData.username === "irfanusuf33@gmail.com" && formData.password) {

        // alert("login Succesfull")
        navigation.navigate("Profile")
      } else {
        // alert("Password Incorrect")
      }

    } catch (error) {
      console.error(error);
    }
  };
  return (
    <View style={styles.container}>
      <Text style={{ fontSize: 32, marginBottom: 24, fontWeight: 600 }}>
        <Icon name="user" size={30} color="#007AFF" /> Login
      </Text>

      <View style={{ width: "80%", marginBottom: 16 }}>
        <Text style={{ marginBottom: 4 }}>Username or Email</Text>
        <View style={{ borderWidth: 1, borderColor: "#ccc", borderRadius: 4 }}>
          <TextInput
            style={{ height: 40, paddingHorizontal: 8 }}
            placeholder="username or user@example.com"
            autoCapitalize="none"
            value={formData.username}
            onChangeText={(text) => setFormData({ ...formData, username: text })}
          />
        </View>
      </View>

      <View style={{ width: "80%", marginBottom: 24 }}>
        <Text style={{ marginBottom: 4 }}>Password  Min (8 characters)</Text>
        <View style={{ borderWidth: 1, borderColor: "#ccc", borderRadius: 4 }}>
          <TextInput
            style={{ height: 40, paddingHorizontal: 8 }}
            placeholder="Enter password"
            secureTextEntry
            onChangeText={(text) => setFormData({ ...formData, password: text })}
          />
        </View>
      </View>

      <View style={{ flexDirection: "row", gap: 2, marginBottom: 10 }}>
        <Text> Dont have an account go to </Text>
        <Text
          style={{ color: "blue" }}
          onPress={() => {
            navigation.navigate("Register");
          }}
        >
          Register
        </Text>


      </View>


      <TouchableOpacity
        style={{ backgroundColor: "#007AFF", padding: 8, paddingHorizontal: 30, borderRadius: 4 }}
        onPress={handleLogin}
      >
        <Text
          style={{ color: "#fff", textAlign: "center", fontWeight: "bold" }}
        >
          {loading ? "Loading..." : "Login"}
        </Text>
      </TouchableOpacity>





      <View style={{ flexDirection: "row", alignItems: "center", marginVertical: 20, width: "80%" }}>
        <View style={{ flex: 1, height: 1, backgroundColor: "#ccc" }} />
        <Text style={{ marginHorizontal: 10, color: "#888" , fontSize : 18 }}>or</Text>
        <View style={{ flex: 1, height: 1, backgroundColor: "#ccc" }} />
      </View>


      <TouchableOpacity
        style={{
          flexDirection: "row",
          alignItems: "center",
          backgroundColor: "#fff",
          borderWidth: 1,
          borderColor: "#ccc",
          borderRadius: 4,
          paddingVertical: 10,
          paddingHorizontal: 20,
          width: "80%",
          justifyContent: "center",
          marginBottom: 16,
          elevation: 2, // for subtle shadow on Android
        }}
        onPress={() => {
          // Handle Google login here
        }}
      >
        <Icon name="google" size={22} color="#DB4437" style={{ marginRight: 10 }} />
        <Text style={{ color: "#333", fontWeight: "bold" }}>Login with Google</Text>
      </TouchableOpacity>



    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    alignItems: 'center',
    paddingTop: 150
    // justifyContent: 'center',
  },
});
