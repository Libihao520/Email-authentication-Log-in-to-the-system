import request from '@/utils/request'

//用户注册接口
export const userRegisterService = ({ username, password, repassword,email,authcode }) =>
  request.post('/login/add', { username, password, repassword,email,authcode })
//用户登录接口
export const userLoginService = ({ username, password }) =>
  request.post('/login/GetToken', { username, password })

//获取用户基本信息
export const userGetInfoService = () => request.get('/login/userinfo')

//传邮箱，发送验证码
export const SendVerificationCode =(email)=>request.get('/login/SendVerificationCode',{params:{email}})
