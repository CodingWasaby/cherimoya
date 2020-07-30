import axios from "axios";
import { message } from "antd";

export const uesInstance = (params?: any) => {
    const uesApi = axios.create({
        baseURL: "",
        ...params
    });
    uesApi.interceptors.response.use(function (response) {
        return response;

    }, function (error: any) {
        // 未返回错误
        if (error.response === undefined) {
            message.error("操作失败，请联系管理员");
            return (Promise.reject(error));
        }
        return (Promise.reject(error));
    });

    return uesApi;
}

