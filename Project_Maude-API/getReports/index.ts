import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    const results = [];

    const result = await pool.request()
            .execute("getReports");

            if (result.recordsets.length == 0){
                throw new Error("Error in base api request");}

            for(const report of result.recordset){
                results.push({
                    reportId: report["ReportId"],
                    reportImageUrl: report["ReportImageUrl"],
                    reportScore: report["ReportScore"],
                    reportTitle: report["ReportTitle"],
                    barcode: report["Barcode"]
                })
            }
            context.res.end(JSON.stringify(results)); 
}

export default httpTrigger;
