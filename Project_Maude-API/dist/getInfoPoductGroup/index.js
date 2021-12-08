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
        const { productGroupName } = req.body;
        if (productGroupName) {
            const result = yield pool.request()
                .input("ProductGroupName", productGroupName)
                .execute("getInfo");
            if (result.recordsets.length == 0) {
                throw new Error("Error in base api request");
            }
            context.res.end(JSON.stringify(result.recordset.map(row => ({
                productGroupName: row["ProductGroupName"],
                recycleSteps: row["RecycleSteps"],
                imageURLID: row["ImageURLID"],
                material: row["Material"],
                isRecyclable: row["IsRecyclable"],
                materialImageID: row["MaterialImageID"],
                recycleBin: row["RecycleBin"],
                warning: row["Warning"]
            }))));
        }
        else {
            context.res = {
                body: "Please pass a valid product group name"
            };
        }
    });
};
exports.default = httpTrigger;
//# sourceMappingURL=index.js.map