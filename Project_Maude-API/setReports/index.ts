import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();


    const  {ReportImageID, ReportTitle, Barcode}  = req.body;
    
    
    let keyword = ReportTitle.split(' ');
    var score= 50;
    var temp = 0;
    //Assigning an initial score to a report by getting the avarege search frequency for every word in the report name
    for (const word of keyword){
        
            const result = await pool.request()
                    .input("SearchTerm", word)
                    .execute("getSearchCount");
                    if(result.recordset.length == 0){}
                    else{
                        score = score + Number(result.recordset[0].Frequency);
                        
                    }
                    temp++;
                    
        
    }
    score = score/temp;
    //setting the report
    await pool.request()
                    .input("ReportImageID", ReportImageID,)
                    .input( "ReportScore",  score,)
                    .input("ReportTitle",  ReportTitle,)
                    .input("Barcode",      Barcode)
                    .execute("addReport");

                    context.res = {
                        status:200
                    };
}
export default httpTrigger;