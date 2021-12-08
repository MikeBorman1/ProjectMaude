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
        var matString = "";
        const interesults = [];
        const result = yield pool.request()
            .execute("getProducts");
        for (const productId of result.recordset) {
            const materialsResult = yield pool.request()
                .input("ProductId", productId.ProductId)
                .execute("getMaterials");
            for (const material of materialsResult.recordset) {
                matString = matString + material.Material + ",";
            }
            var l = interesults.push({
                productId: productId.ProductId,
                productName: productId.ProductName,
                productPhotoId: productId.ProductPhotoId,
                barcode: productId.Barcode,
                isVerified: productId.IsVerified,
                flag: productId.Flag,
                matString,
            });
            matString = "";
        }
        context.res.end(JSON.stringify(interesults));
    });
};
exports.default = httpTrigger;
//# sourceMappingURL=index.js.map