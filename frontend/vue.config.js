module.exports = {
  devServer: {
    proxy: {
      '^/api': {
        target: 'http://localhost:5083',
        changeOrigin: true
      }
    }
  },
  configureWebpack: {
    resolve: {
      alias: {
        'jwt-decode': require.resolve('jwt-decode')
      }
    }
  }
}
