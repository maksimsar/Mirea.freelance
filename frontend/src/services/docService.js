import axios from 'axios';

const api = axios.create({
  baseURL: '/api/gitdocs',
  headers: { Accept: 'application/json' }
});

export default {
  list(folder) { return api.get('', { params: folder ? { folder } : {} }); },
  download(path) { return api.get(`/${encodeURIComponent(path)}`, { responseType: 'blob' }); },
  upload(path, file) {
    const form = new FormData();
    form.append('file', file);
    return api.post('', form, { params: { path } });
  },
  history(path) { return api.get(`/history/${encodeURIComponent(path)}`); },
  delete(path) { return api.delete(`/${encodeURIComponent(path)}`); },
  stats() { return axios.get('/api/gitdocs/stats'); },       // new
  testConnection() { return axios.get('/api/gitdocs'); }       // proxy to test
};
