import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    var matString = "";
    const interesults = [];
    const result = await pool.request()
            .execute("getProducts");

            for(const productId of result.recordset) {
                
                const materialsResult = await pool.request()
                    .input("ProductId", productId.ProductId)
                    .execute("getMaterials");

                    for(const material of materialsResult.recordset){
                     
                        matString = matString+material.Material + ",";
                    }

                    var l = interesults.push({
                        productId: productId.ProductId as number,
                        productName: productId.ProductName as string,
                        productPhotoId: productId.ProductPhotoId as string,
                        barcode: productId.Barcode as string,
                        isVerified: productId.IsVerified as boolean,
                        flag: productId.Flag as boolean,
                        matString,
                            })
                matString = "";
            }
            context.res.end(JSON.stringify(interesults));
};

export default httpTrigger;
