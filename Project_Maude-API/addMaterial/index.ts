import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();

    const {material, materialImageId, isRecyclable, recycleCodeID, WarningId } = req.body;
    
    const MaterialId = await pool.request()
    .input("Material", material)
    .input("IsRecyclable", isRecyclable)
    .input("MaterialImageId", materialImageId)
    .input("RecycleCodeID", recycleCodeID)
    .execute("addMaterial");
    
    for(const id of WarningId){
        await pool.request()
        .input("WarningId",id)
        .input("MaterialId",MaterialId.recordset[0].MaterialId)
        .execute("addWarningLink")
    }
    
    

}
export default httpTrigger;
