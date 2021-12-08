"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const sql = require("mssql");
const httpTrigger = function (context, req) {
    return __awaiter(this, void 0, void 0, function* () {
        context.log('HTTP trigger function processed a request.');
        const connectionString = process.env.DB_CON;
        if (connectionString == undefined)
            throw new Error("Environment variable DB_CON not defined");
        const pool = yield new sql.ConnectionPool(connectionString).connect();
        const { productName, productPhotoId, barcode, materials } = req.body;
        var keywords = [];
        // Takes in product info and a list of materials
        //Getting product ID while also setting the product into the db
        const ProductId = yield pool.request()
            .input("ProductName", productName)
            .input("Barcode", barcode)
            .input("ProductPhotoId", productPhotoId)
            .execute("addProduct");
        let name = productName.split(' ');
        let mat = materials.split(',');
        //Placing every word in product name into a list
        name.forEach(element => { keywords.push(element); });
        if (materials) {
            yield pool.request()
                .input("ProductId", ProductId.recordset[0].ProductId)
                .execute("delMatProdLink");
            //looping over materials
            for (const material of mat) {
                if (material != "") {
                    //grabbing materialID as that is not passsed in
                    var MaterialId = yield pool.request()
                        .input("Material", material)
                        .execute("getMaterialId");
                    var a = MaterialId.recordset[0].MaterialId;
                    //linking the product to the material in the join table
                    yield pool.request()
                        .input("MaterialId", MaterialId.recordset[0].MaterialId)
                        .input("ProductId", ProductId.recordset[0].ProductId)
                        .execute("setMaterialProductLink");
                    keywords.push(material);
                }
            }
            // linking keywords to product
            for (const word of keywords) {
                if (word != "") {
                    yield pool.request()
                        .input("Keyword", word)
                        .input("ProductId", ProductId.recordset[0].ProductId)
                        .execute("addKeywords");
                }
            }
        }
    });
};
exports.default = httpTrigger;
//# sourceMappingURL=index.js.map