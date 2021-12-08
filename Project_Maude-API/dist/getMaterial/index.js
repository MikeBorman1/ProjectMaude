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
        const { materialId } = req.body;
        const material = yield pool.request()
            .input("MaterialId", materialId)
            .execute("getMaterial");
        const warningsResult = yield pool.request()
            .input("MaterialId", material.recordset[0].MaterialId)
            .execute("getWarning");
        const warnings = warningsResult.recordset.map((row) => ({ warning: row["Warning"],
            warningId: row["WarningId"],
        }));
        context.res.end(JSON.stringify({
            materialId: material.recordset[0].MaterialId,
            material: material.recordset[0].Material,
            isRecyclable: material.recordset[0].IsRecyclable,
            materialImageId: material.recordset[0].MaterialImageId,
            recycleBin: material.recordset[0].RecycleBin,
            recycleCode: material.recordset[0].RecycleCodeID,
            warnings
        }));
    });
};
exports.default = httpTrigger;
//# sourceMappingURL=index.js.map