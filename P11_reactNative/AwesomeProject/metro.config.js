const { getDefaultConfig, mergeConfig } = require('@react-native/metro-config');

const defaultConfig = getDefaultConfig(__dirname);

const config = {
  resolver: {
    assetExts: [...defaultConfig.resolver.assetExts, 'ttf'], // add 'ttf' here if not already present
  },
};

module.exports = mergeConfig(defaultConfig, config);
