import { StatusBar } from 'expo-status-bar';
import { useState } from 'react';
import { StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';

export default function LoginScreen() {
  const [loading, setLoading] = useState(false);

const handleLogin =  async () =>{

try {
   



} catch (error) {
  console.error(error)
}


}
  return (
    <View style={styles.container}>
      <Text style={{ fontSize: 32, marginBottom: 24 }}>Login</Text>

      {/* Username */}
      <View style={{ width: '80%', marginBottom: 16 }}>
        <Text style={{ marginBottom: 4 }}>Username</Text>
        <View style={{ borderWidth: 1, borderColor: '#ccc', borderRadius: 4 }}>
          <TextInput
            style={{ height: 40, paddingHorizontal: 8 }}
            placeholder="Enter username"
            autoCapitalize="none"
          />
        </View>
      </View>

      {/* Password */}
      <View style={{ width: '80%', marginBottom: 24 }}>
        <Text style={{ marginBottom: 4 }}>Password</Text>
        <View style={{ borderWidth: 1, borderColor: '#ccc', borderRadius: 4 }}>
          <TextInput
            style={{ height: 40, paddingHorizontal: 8 }}
            placeholder="Enter password"
            secureTextEntry
          />
        </View>
      </View>

      {/* Login Button */}
      <TouchableOpacity style={{ backgroundColor: '#007AFF', padding: 12, borderRadius: 4 }} onPress={handleLogin}>
        <Text style={{ color: '#fff', textAlign: 'center', fontWeight: 'bold' }}>
          {loading ? 'Loading...' : 'Login'}
        </Text>
      </TouchableOpacity>

      <StatusBar style="auto" />
    </View>
  );
}





const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
