import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    const { productId } = req.body;
    var mats = [];
    const product = await pool.request()
                    .input("ProductId", productId)
                    .execute("getProduct");
                    
                    const materialsResult = await pool.request()
                    .input("ProductId", product.recordset[0].ProductId)
                    .execute("getMaterials");
                    for(const material of materialsResult.recordset){
                    
                        mats.push(material.Material)
                    };

                    context.res.end(JSON.stringify({
                        productId: product.recordset[0].ProductId as number,
                        productName: product.recordset[0].ProductName as string,
                        barcode: product.recordset[0].Barcode as string,
                        productPhotoId: product.recordset[0].ProductPhotoId as string,
                        flag: product.recordset[0].Flag as boolean,
                        isVerified: product.recordset[0].IsVerified as boolean,
                        mats
                    }
                        
                    ))
};

export default httpTrigger;
