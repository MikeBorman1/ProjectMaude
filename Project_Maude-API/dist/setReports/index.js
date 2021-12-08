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
        const { ReportImageID, ReportTitle, Barcode } = req.body;
        let keyword = ReportTitle.split(' ');
        var score = 50;
        var temp = 0;
        //Assigning an initial score to a report by getting the avarege search frequency for every word in the report name
        for (const word of keyword) {
            const result = yield pool.request()
                .input("SearchTerm", word)
                .execute("getSearchCount");
            if (result.recordset.length == 0) { }
            else {
                score = score + Number(result.recordset[0].Frequency);
            }
            temp++;
        }
        score = score / temp;
        //setting the report
        yield pool.request()
            .input("ReportImageID", ReportImageID)
            .input("ReportScore", score)
            .input("ReportTitle", ReportTitle)
            .input("Barcode", Barcode)
            .execute("addReport");
        context.res = {
            status: 200
        };
    });
};
exports.default = httpTrigger;
//# sourceMappingURL=index.js.map