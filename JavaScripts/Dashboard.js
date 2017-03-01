

$(document).ready(function () {
    var chart = new CanvasJS.Chart("chartContainer", {
        title: {
            text: "All Academy Estimates"
        },
        data: [
		{
		    // Change type to "doughnut", "line", "splineArea", etc.
		    type: "column",
		    dataPoints: [
				{ label: "Total", y: 10 },
				{ label: "Approved", y: 15 },
				{ label: "NotApproved", y: 25 }
		    ]
		}
        ]
    });
    chart.render();
});