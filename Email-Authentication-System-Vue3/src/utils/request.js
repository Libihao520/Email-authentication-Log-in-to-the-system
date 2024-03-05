import axios from 'axios'
import { useUserStore } from '@/stores'
import { ElMessage } from 'element-plus'
import router from '../router'
const baseURL = 'http://localhost:5157/api'

const instance = axios.create({
  //基础地址，超时时间
  baseURL,
  timeout: 10000
})
//请求拦截器
instance.interceptors.request.use(
  (config) => {
    //携带token
    const useStore = useUserStore()
    if (useStore.token) {
      config.headers.Authorization = 'Bearer ' + useStore.token
    }
    return config
  },
  (err) => Promise.reject(err)
)
//响应拦截器
instance.interceptors.response.use(
  (res) => {
    if (res.data.code === 0) {
      return res
    }
    //处理业务失败,摘取核心响应数据
    ElMessage.error(res.data.message || '服务异常')
    return Promise.reject(res.data)
  },

  (err) => {
    // 处理401错误
    //错误特殊情况 权限不足，或token过期，拦截到login
    if (err.response?.status === 401) {
      router.push('/login')
    }
    //错误默认情况
    ElMessage.error(err.response.data.message || '服务异常')
    return Promise.reject(err)
  }
)

export default instance
export { baseURL }
