const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "https://localhost:5001",
    secure: false,
    logLevel: "debug",
    changeOrigin: true,
    pathRewrite: { "^/api": "" }
  }
]

module.exports = PROXY_CONFIG;
