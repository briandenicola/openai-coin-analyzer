import React, { useState } from "react";
import { View, Text, Button, Image, StyleSheet, ActivityIndicator } from "react-native";
import * as ImagePicker from "expo-image-picker";

export default function App() {
  const [selectedImage, setSelectedImage] = useState(null);
  const [analysisResult, setAnalysisResult] = useState(null);
  const [loading, setLoading] = useState(false);

  // Function to pick an image from the device
  const pickImage = async () => {
    let result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ['images'],
      allowsEditing: false,
      quality: 1,
    });

    if (!result.canceled) {
      setSelectedImage(result.assets[0]);
      setAnalysisResult(null); // Reset previous result
    }
  };

  // Function to analyze the image via an API
  const analyzeImage = async () => {
    if (!selectedImage) return;
    
    setLoading(true);
    
    let formData = new FormData();
    formData.append("file", selectedImage.file, selectedImage.fileName);

    try {
      const api_uri = process.env.EXPO_PUBLIC_API_URL;
      if (!api_uri) {
        throw new Error("API URL is not defined");
      }
      const response = await fetch(api_uri, {
        method: "POST",
        headers:  {
          "Accept": "*/*",
          "Ocp-Apim-Subscription-Key": process.env.EXPO_PUBLIC_API_KEY,
          //"Content-Type": "multipart/form-data",},
        },
        body: formData,
      });

      const analysis = await response.json();
      console.log("Analysis result:", analysis);
      setAnalysisResult(analysis.result);
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
        <Image source={{ uri: selectedImage.uri }} style={styles.image} />
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
    width: 600,
    height: 300,
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