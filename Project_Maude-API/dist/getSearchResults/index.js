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
        const { keywords } = req.body;
        const results = [];
        const interesults = [];
        let keyword = keywords.split(' ');
        var mats = [];
        if (keywords) {
            // for every keyword
            for (const word of keyword) {
                //returns a search result(productName,ProductImage......)
                const result = yield pool.request()
                    .input("Keyword", word)
                    .execute("getSearchResults");
                if (result.recordsets.length == 0) {
                    throw new Error("Error in base api request");
                }
                for (const productId of result.recordset) {
                    //gets the materials for every product
                    const materialsResult = yield pool.request()
                        .input("ProductId", productId.ProductId)
                        .execute("getMaterials");
                    mats = [];
                    //gets every warning linked to the material
                    for (const material of materialsResult.recordset) {
                        const warningsResult = yield pool.request()
                            .input("MaterialId", material.MaterialId)
                            .execute("getWarning");
                        const warnings = warningsResult.recordset.map((row) => ({ warning: row["Warning"],
                        }));
                        //push everything onto a materials list
                        var m = mats.push({ material: material["Material"],
                            imageID: material["MaterialImageId"],
                            isRecyclable: material["IsRecyclable"],
                            recycleBin: material["RecycleBin"],
                            isBin: material["IsBin"],
                            recycleCodeID: material["RecycleCodeID"],
                            warnings });
                    }
                    // push onto the searchresult list
                    var l = interesults.push({
                        productId: productId.ProductId,
                        productName: productId.ProductName,
                        productPhotoId: productId.ProductPhotoId,
                        isVerified: productId.IsVerified,
                        flagged: productId.Flag,
                        mats
                    });
                }
            }
        }
        else {
            context.res = {
                body: "Please pass a valid keyword"
            };
        }
        //logic to make sure there are no duplictates
        if (keyword.length > 1) {
            for (const compare of interesults) {
                var count = 0;
                for (const item of interesults) {
                    if (compare.productId == item.productId) {
                        count = count + 1;
                    }
                }
                if (count > 1) {
                    if (!results.some(item => item.productId == compare.productId)) {
                        results.push(compare);
                    }
                }
            }
            context.res.end(JSON.stringify(results));
        }
        else {
            context.res.end(JSON.stringify(interesults));
        }
    });
};
exports.default = httpTrigger;
//# sourceMappingURL=index.js.map