import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    const { materialId } = req.body;

    const material = await pool.request()
                    .input("MaterialId", materialId)
                    .execute("getMaterial");
                    
                    
                        const warningsResult = await pool.request()
                        .input("MaterialId", material.recordset[0].MaterialId)
                        .execute("getWarning");

                        const warnings = warningsResult.recordset.map((row) => ({ warning: row["Warning"] as string, 
                                                                                  warningId: row["WarningId"] as number,
                                                                                }));
                    context.res.end(JSON.stringify({
                        materialId: material.recordset[0].MaterialId as number,
                        material: material.recordset[0].Material as string,
                        isRecyclable: material.recordset[0].IsRecyclable as boolean,
                        materialImageId: material.recordset[0].MaterialImageId as string,
                        recycleBin: material.recordset[0].RecycleBin as string,
                        recycleCode: material.recordset[0].RecycleCodeID as number,
                        warnings}
                        
                    ))
};

export default httpTrigger;
