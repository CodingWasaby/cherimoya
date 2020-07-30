import { uesInstance } from "./base";

export const getPlan = async () => {
    return await uesInstance().get("/home/GetPlanForF").then(res => res.data);
}