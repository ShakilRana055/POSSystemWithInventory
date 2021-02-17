function percentToDecimal(percent, amount) {
    if (percent > 0) {

        let value = percent * amount;
        return value / 100;

    }
}

function decimalToPercent(decimal, amount) {
    if (decimal > 0) {

        let value = decimal / amount;
        return value * 100; 

    }
}