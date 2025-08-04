import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';
import Icon from 'react-native-vector-icons/FontAwesome';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { ParamList } from '../App';

type Props = NativeStackScreenProps<ParamList, 'Home'>;

export default function HomeScreen({ navigation }: Props) {
  return (
    <View style={styles.container}>

      <View style={styles.header}>
        <Icon name="bitcoin" size={32} color="#F7931A" />
        <Text style={styles.headerText}>Crypto Wallet </Text>
      </View>


      <Text style={styles.greeting}>Welcome back, User!</Text>


      <View style={styles.balanceCard}>
        <Text style={styles.balanceLabel}>Total Balance</Text>
        <Text style={styles.balanceValue}>$12,345.67</Text>
      </View>


      <View style={styles.actionsRow}>
        <TouchableOpacity style={styles.actionBtn} onPress={() => navigation.navigate('Profile')}>
          <Icon name="user" size={24} color="#007AFF" />
          <Text style={styles.actionText}>Profile</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionBtn} onPress={() => navigation.navigate('Camera')}>
          <Icon name="camera" size={24} color="#28a745" />
          <Text style={styles.actionText}>Camera</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionBtn} onPress={() => navigation.navigate('Login')}>
          <Icon name="sign-in" size={24} color="#ff6347" />
          <Text style={styles.actionText}>Login</Text>
        </TouchableOpacity>
      </View>


      <View style={styles.quickActions}>
        <TouchableOpacity style={styles.quickBtn}>
          <Icon name="arrow-up" size={20} color="#fff" />
          <Text style={styles.quickBtnText}>Send</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.quickBtn}>
          <Icon name="arrow-down" size={20} color="#fff" />
          <Text style={styles.quickBtnText}>Receive</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.quickBtn}>
          <Icon name="exchange" size={20} color="#fff" />
          <Text style={styles.quickBtnText}>Swap</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#f9f9f9', alignItems: 'center', paddingTop: 60 },
  header: { flexDirection: 'row', alignItems: 'center', marginBottom: 20 },
  headerText: { fontSize: 28, fontWeight: 'bold', marginLeft: 10, color: '#222' },
  greeting: { fontSize: 18, color: '#555', marginBottom: 20 },
  balanceCard: {
    backgroundColor: '#fff',
    width: '85%',
    borderRadius: 12,
    padding: 24,
    alignItems: 'center',
    marginBottom: 30,
    elevation: 3,
    shadowColor: '#000',
    shadowOpacity: 0.05,
    shadowRadius: 8,
    shadowOffset: { width: 0, height: 2 },
  },
  balanceLabel: { color: '#888', fontSize: 16 },
  balanceValue: { fontSize: 32, fontWeight: 'bold', color: '#222', marginTop: 8 },
  actionsRow: { flexDirection: 'row', justifyContent: 'space-between', width: '85%', marginBottom: 30 },
  actionBtn: { alignItems: 'center', flex: 1 },
  actionText: { marginTop: 6, color: '#333', fontSize: 14 },
  quickActions: { flexDirection: 'row', justifyContent: 'space-between', width: '85%' },
  quickBtn: {
    backgroundColor: '#007AFF',
    flex: 1,
    marginHorizontal: 6,
    borderRadius: 8,
    paddingVertical: 14,
    alignItems: 'center',
    justifyContent: 'center',
  },
  quickBtnText: { color: '#fff', marginTop: 4, fontWeight: 'bold' },
});