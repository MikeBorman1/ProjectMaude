import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();

    const {productName, productPhotoId, barcode, materials } = req.body;
    var keywords = [];
    // Takes in product info and a list of materials
    //Getting product ID while also setting the product into the db
    const ProductId = await pool.request()
    .input("ProductName", productName)
    .input("Barcode", barcode)
    .input("ProductPhotoId", productPhotoId)
    .execute("addProduct");
    
    let name = productName.split(' ');
    let mat = materials.split(',');
    //Placing every word in product name into a list
    name.forEach(element => {keywords.push(element)}); 
    
    if(materials){
        await pool.request()
            .input("ProductId", ProductId.recordset[0].ProductId)
            .execute("delMatProdLink")
        //looping over materials
        for(const material of mat){
            if(material != ""){
            //grabbing materialID as that is not passsed in
            var MaterialId = await pool.request()
            .input("Material", material)
            .execute("getMaterialId");
            var a = MaterialId.recordset[0].MaterialId;
            //linking the product to the material in the join table
            await pool.request()
            .input("MaterialId", MaterialId.recordset[0].MaterialId)
            .input("ProductId", ProductId.recordset[0].ProductId)
            .execute("setMaterialProductLink");
            keywords.push(material)
            }
        }
        // linking keywords to product
        for(const word of keywords){
            if(word !=""){
                await pool.request()
                .input("Keyword", word)
                .input("ProductId", ProductId.recordset[0].ProductId)
                .execute("addKeywords");
            }
        }
    }
}
export default httpTrigger;
