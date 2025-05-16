import axios from 'axios'
import FileHandlerFactory from './FileHandlerFactory' 

const api = axios.create({
  baseURL: '/api/gitdocs',
  headers: { Accept: 'application/json' }
})

class DocService {
  list(folder) {
    return api.get('', { params: folder ? { folder } : {} })
  }

  download(path) {
    return api.get(`/${encodeURIComponent(path)}`, { responseType: 'blob' })
  }

  async upload(path, file) {
    const ext = path.slice(path.lastIndexOf('.'))
    const handler = FileHandlerFactory.create(ext)
    await handler.validate(path, file)

    const form = new FormData()
    form.append('file', file)
    return api.post('', form, { params: { path } })
  }

  history(path) {
    return api.get(`/history/${encodeURIComponent(path)}`)
  }

  delete(path) {
    return api.delete(`/${encodeURIComponent(path)}`)
  }

  stats(folder) {
    return api.get('/stats', { params: folder ? { folder } : {} })
  }

  createFolder(path) {
    return api.post('/folder', null, { params: { path } })
  }

  rename(oldPath, newPath) {
    return api.post('/rename', null, { params: { oldPath, newPath } })
  }

  move(oldPath, newPath) {
    return api.post('/move', null, { params: { oldPath, newPath } })
  }

  testConnection() {
    return axios.get('/api/gitdocs')
  }
}

export default new DocService()
