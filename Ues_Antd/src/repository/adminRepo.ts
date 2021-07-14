import { uesInstance } from "./base";

export const RgisterSumbit = async (username: string, password: string) => {
    return await uesInstance().post("/login/RgisterSumbit", {
        Email: username,
        Password: password
    }).then(res => res.data as string);
}
export const LoginSumbit = async (username: string, password: string) => {
    return await uesInstance().post("/login/LoginSumbit", {
        Email: username,
        Password: password
    }).then(res => res.data as string);
}

export const ReSetSumbit = async (username: string, password: string) => {
    return await uesInstance().post("/login/SendForgetMail", {
        Email: username,
        Password: password
    }).then(res => res.data as string);
}

