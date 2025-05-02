module.exports = {
  devServer: {
    proxy: {
      '^/api': {
        target: 'http://localhost:5083',
        changeOrigin: true
      }
    }
  }
}
