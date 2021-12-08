import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    const { reportId } = req.body;

    const report = await pool.request()
                    .input("ReportId", reportId)
                    .execute("getReport");

                    context.res.end(JSON.stringify({
                        reportId: report.recordset[0].ReportId as number,
                        reportTitle: report.recordset[0].ReportTitle as string,
                        barcode: report.recordset[0].Barcode as string,
                        reportImageUrl: report.recordset[0].ReportImageUrl as string}
                        
                    ))
};

export default httpTrigger;
