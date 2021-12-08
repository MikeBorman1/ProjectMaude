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
        const results = [];
        // Gets a list of all materials in the db
        const Materials = yield pool.request()
            .execute("getAllMaterials");
        for (const material of Materials.recordset) {
            const warningsResult = yield pool.request()
                .input("MaterialId", material.MaterialId)
                .execute("getWarning");
            var warnings = "";
            for (const warning of warningsResult.recordset) {
                warnings = warnings + warning.Warning + ", ";
            }
            results.push({
                material: material["Material"],
                materialImageId: material["MaterialImageId"],
                isRecyclable: material["IsRecyclable"],
                recycleBin: material["RecycleBin"],
                isBin: material["IsBin"],
                materialId: material["MaterialId"],
                recycleCodeId: material["RecycleCodeId"],
                warnings
            });
        }
        context.res.end(JSON.stringify(results));
    });
};
exports.default = httpTrigger;
//# sourceMappingURL=index.js.map