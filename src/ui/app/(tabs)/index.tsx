import React, { useState } from "react";
import { View, Text, Button, Image, StyleSheet, ActivityIndicator } from "react-native";
import * as ImagePicker from "expo-image-picker";
import Constants from "expo-constants";

export default function App() {
  const [selectedImage, setSelectedImage] = useState(null);
  const [analysisResult, setAnalysisResult] = useState(null);
  const [loading, setLoading] = useState(false);

  // Function to pick an image from the device
  const pickImage = async () => {
    let result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      quality: 1,
    });

    if (!result.canceled) {
      setSelectedImage(result.assets[0].uri);
      setAnalysisResult(null); // Reset previous result
    }
  };

  // Function to analyze the image via an API
  const analyzeImage = async () => {
    if (!selectedImage) return;
    
    setLoading(true);
    
    let formData = new FormData();
    formData.append("file", {
      uri: selectedImage,
      name: "image.jpg",
      type: "image/jpeg",
    });

    try {
      const response = await fetch(process.env.EXPO_PUBLIC_API_URL, {
        method: "POST",
        headers: {
          "Content-Type": "multipart/form-data",
        },
        body: formData,
      });

      const result = await response.json();
      setAnalysisResult(result);
    } catch (error) {
      console.error("Error analyzing image:", error);
      setAnalysisResult({ error: "Failed to analyze the image." });
    }

    setLoading(false);
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Roman Imperial Coin Image Analysis</Text>
      
      <Button title="Select Image" onPress={pickImage} />
      
      {selectedImage && (
        <Image source={{ uri: selectedImage }} style={styles.image} />
      )}

      {selectedImage && !loading && (
        <Button title="Analyze Image" onPress={analyzeImage} />
      )}

      {loading && <ActivityIndicator size="large" color="blue" />}
      
      {analysisResult && (
        <View style={styles.resultContainer}>
          <Text style={styles.resultText}>Analysis Result:</Text>
          <Text>{JSON.stringify(analysisResult, null, 2)}</Text>
        </View>
      )}
    </View>
  );
}

// Styles for the UI
const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
    padding: 20,
    backgroundColor:'#f5f5f5',
  },
  title: {
    fontSize: 20,
    fontWeight: "bold",
    marginBottom: 10,
  },
  image: {
    width: 200,
    height: 200,
    marginTop: 10,
    borderRadius: 10,
  },
  resultContainer: {
    marginTop: 20,
    backgroundColor: "#fff",
    padding: 10,
    borderRadius: 5,
    elevation: 3,
  },
  resultText: {
    fontWeight: "bold",
    marginBottom: 5,
  },
});