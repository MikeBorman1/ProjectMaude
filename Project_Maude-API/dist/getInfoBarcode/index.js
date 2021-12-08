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
        const { barcode } = req.body;
        var mats = [];
        const results = [];
        //for comments look at getSearchResults, only difference is we are passing in a barcode not a keyword
        if (barcode) {
            const result = yield pool.request()
                .input("Barcode", barcode)
                .execute("getProductId");
            if (result.recordsets.length == 0) {
                throw new Error("Error in base api request");
            }
            for (const productId of result.recordset) {
                const result1 = yield pool.request()
                    .input("productID", productId.ProductId)
                    .execute("getInfo");
                if (result1.recordsets.length == 0) {
                    throw new Error("Error in base api request");
                }
                const materialsResult = yield pool.request()
                    .input("ProductId", productId.ProductId)
                    .execute("getMaterials");
                mats = [];
                for (const material of materialsResult.recordset) {
                    const warningsResult = yield pool.request()
                        .input("MaterialId", material.MaterialId)
                        .execute("getWarning");
                    const warnings = warningsResult.recordset.map((row) => ({ warning: row["Warning"],
                    }));
                    var m = mats.push({ material: material["Material"],
                        imageID: material["MaterialImageId"],
                        isRecyclable: material["IsRecyclable"],
                        recycleBin: material["RecycleBin"],
                        isBin: material["IsBin"],
                        recycleCodeID: material["RecycleCodeID"],
                        warnings });
                }
                if (!results.some(item => item.ProductId == productId.ProductId)) {
                    results.push({
                        productId: productId.ProductId,
                        productName: result1.recordset[0]["ProductName"],
                        productPhotoId: result1.recordset[0]["ProductPhotoId"],
                        isVerified: result1.recordset[0]["IsVerified"],
                        flagged: result1.recordset[0]["Flag"],
                        mats
                    });
                }
            }
        }
        else {
            context.res = {
                body: "Please pass a valid product group name"
            };
        }
        context.res.end(JSON.stringify(results));
    });
};
exports.default = httpTrigger;
//# sourceMappingURL=index.js.map