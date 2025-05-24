function printCurrentPage() {
    const tableContainer = document.querySelector(".table-container").cloneNode(true);

    // Удалить последний столбец "Удалить" из заголовка
    const headerRow = tableContainer.querySelector("thead tr");
    if (headerRow.lastElementChild?.innerText.trim() === "Удалить") {
        const rows = tableContainer.querySelectorAll("tbody tr");
        rows.forEach(row => {
            if (row.children.length > 0) {
                row.removeChild(row.lastElementChild);
            }
        });
        headerRow.removeChild(headerRow.lastElementChild);
    }


    const colgroup = document.createElement("colgroup");
    colgroup.innerHTML = `
        <col style="width: 9%;">
        <col style="width: 9%;">
        <col style="width: 7%;">
        <col style="width: 7%;">
        <col style="width: 7%;">
        <col style="width: 9%;">
        <col style="width: 17%;">
        <col style="width: 7%;">
        <col style="width: 5%;">
        <col style="width: 8%;">
        <col style="width: 7%;">
        <col style="width: 8%;">
    `;
    const table = tableContainer.querySelector("table");
    if (table) {
        table.insertBefore(colgroup, table.querySelector("thead"));
    }
    console.log(table);
    const style = `
        <style>
            @page {
                size: landscape;
                margin: 1cm;
            }
            * {
                box-sizing: border-box;
            }
            body {
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                margin: 0;
                padding: 0;
            }
            table {
                width: 100%;
                border-collapse: collapse;
                table-layout: fixed;
            }
            th, td {
                border: 1px solid #ccc;
                padding: 3px;
                font-size: 9px;
                vertical-align: middle;
                word-wrap: break-word;
                white-space: normal;
                word-break: break-word;
            }
            th {
                background-color: #f5f5f5;
            }
            h3 {
                text-align: center;
                margin-bottom: 20px;
            }
        </style>
    `;

    const win = window.open(' ', ' ', 'height=700,width=1000');
    win.document.write('<html><head><title>&#8203;</title>');
    win.document.write(style);
    win.document.write('</head><body>');
    win.document.write('<h3>Отчет из журнала титрованных растворов</h3>');
    win.document.write(tableContainer.innerHTML);
    win.document.write('</body></html>');
    console.log(win.document);
    win.document.close();
    win.focus();
    win.print();
    win.close();
}
