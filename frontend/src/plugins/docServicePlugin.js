import DocService from '@/services/docService'

export default {
  install(app) {
    app.config.globalProperties.$docService = DocService
    app.provide('docService', DocService)
  }
}
