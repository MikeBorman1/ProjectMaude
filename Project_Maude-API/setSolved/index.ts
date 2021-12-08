import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();

    const {RecycleSteps,ProductGroupName,KeyWords,ReportId, Materials,Barcode,SolvedImage} = req.body;
    await pool.request()
    .input(
        "ProductGroupName",  ProductGroupName,
        )
    .input("RecycleSteps", RecycleSteps,
            )
    
    .input(
    "KeyWords",  KeyWords,
    )
    
    
    .input(
        "ReportId",      ReportId)
    .input(
        "Barcode",      Barcode)
    .input(
        "Materials",      Materials)
    .input(
        "SolvedImage",      SolvedImage)

    .execute("addSolved");
}

export default httpTrigger;
