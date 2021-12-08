import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();

    const { barcode } = req.body;
    var mats = [];
    const results = [];
//for comments look at getSearchResults, only difference is we are passing in a barcode not a keyword
    if (barcode) {
        const result = await pool.request()
            .input("Barcode", barcode)
            .execute("getProductId");

            if (result.recordsets.length == 0){
                throw new Error("Error in base api request");}

            for(const productId of result.recordset) {
        
                const result1 = await pool.request()
                    .input("productID", productId.ProductId)
                    .execute("getInfo");

                    if (result1.recordsets.length == 0){
                    throw new Error("Error in base api request");}

                const materialsResult = await pool.request()
                    .input("ProductId", productId.ProductId)
                    .execute("getMaterials");

                mats = [];
                for(const material of materialsResult.recordset){
                    const warningsResult = await pool.request()
                    .input("MaterialId", material.MaterialId)
                    .execute("getWarning");

                    const warnings = warningsResult.recordset.map((row) => ({ warning: row["Warning"] as string, 
                                                                            }));

                    
                    
                    var m = mats.push({ material: material["Material"] as string, 
                                        imageID: material["MaterialImageId"] as string,
                                        isRecyclable: material["IsRecyclable"] as boolean,
                                        recycleBin: material["RecycleBin"] as string,
                                        isBin: material["IsBin"] as boolean,
                                        recycleCodeID: material["RecycleCodeID"],
                                        warnings});
                                                                   
                }
                
                if(!results.some(item => item.ProductId == productId.ProductId)){
                    results.push({
                        productId: productId.ProductId as number,
                        productName: result1.recordset[0]["ProductName"] as string,
                        productPhotoId: result1.recordset[0]["ProductPhotoId"] as string,
                        isVerified: result1.recordset[0]["IsVerified"] as boolean,
                        flagged: result1.recordset[0]["Flag"] as boolean,
                        mats
                         }
                    );
                }
                    
                }
        }

    else {
        context.res = {
            body: "Please pass a valid product group name"
        };
    }
    context.res.end(JSON.stringify(results))
    ;

    
};

export default httpTrigger;
