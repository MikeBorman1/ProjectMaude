import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    const results = [];
    // Gets a list of all materials in the db
    const Materials = await pool.request()
    .execute("getAllMaterials");
    for(const material of Materials.recordset){
        const warningsResult = await pool.request()
                        .input("MaterialId", material.MaterialId)
                        .execute("getWarning");
                        var warnings = "";
                        for (const warning of warningsResult.recordset ){
                            warnings = warnings + warning.Warning + ", " 
                        }
        results.push({
            material: material["Material"],
            materialImageId: material["MaterialImageId"],
            isRecyclable: material["IsRecyclable"],
            recycleBin: material["RecycleBin"],
            isBin: material["IsBin"],
            materialId : material["MaterialId"],
            recycleCodeId : material["RecycleCodeId"],
            warnings
        })
    }
    context.res.end(JSON.stringify(results));
}
export default httpTrigger;
