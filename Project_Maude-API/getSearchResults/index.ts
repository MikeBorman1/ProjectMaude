import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();

    const { keywords } = req.body;
    const results = [];
    const interesults = [];
    let keyword = keywords.split(' ');

    var mats = [];
    if(keywords){
        // for every keyword
        for(const word of keyword){
            //returns a search result(productName,ProductImage......)
            const result = await pool.request()
            .input("Keyword", word)
            .execute("getSearchResults");

            if (result.recordsets.length == 0){
                throw new Error("Error in base api request");}

                for(const productId of result.recordset) {

                    //gets the materials for every product
                    const materialsResult = await pool.request()
                    .input("ProductId", productId.ProductId)
                    .execute("getMaterials");

                    mats = [];
                    //gets every warning linked to the material
                    for(const material of materialsResult.recordset){
                        const warningsResult = await pool.request()
                        .input("MaterialId", material.MaterialId)
                        .execute("getWarning");

                        const warnings = warningsResult.recordset.map((row) => ({ warning: row["Warning"] as string, 
                                                                                }));

                        
                        //push everything onto a materials list
                        var m = mats.push({ material: material["Material"] as string, 
                                            imageID: material["MaterialImageId"] as string,
                                            isRecyclable: material["IsRecyclable"] as boolean,
                                            recycleBin: material["RecycleBin"] as string,
                                            isBin: material["IsBin"] as boolean,
                                            recycleCodeID: material["RecycleCodeID"],
                                            warnings});
                                                               
                    }
                

                

                    // push onto the searchresult list
                    var l = interesults.push({
                        productId: productId.ProductId as number,
                        productName: productId.ProductName as string,
                        productPhotoId: productId.ProductPhotoId as string,
                        isVerified: productId.IsVerified as boolean,
                        flagged: productId.Flag as boolean,
                        mats
                            }
                    );
                
                }
            
                
        }
        
    }   

        
              
        

    else {
        context.res = {
            body: "Please pass a valid keyword"
        };
    }

//logic to make sure there are no duplictates
 if(keyword.length > 1){
    for(const compare of interesults) {
    var count = 0
     for(const item of interesults){
         
         if(compare.productId == item.productId){
             count = count +1;
         }        
     }
     
     if(count>1){
        if(!results.some(item => item.productId == compare.productId)){
        results.push(compare)
        }
     }
 } 
 context.res.end(JSON.stringify(results)); 
}
else{
    context.res.end(JSON.stringify(interesults));
}



    
};

export default httpTrigger;
