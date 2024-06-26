﻿class Calculator {
    constructor(previousOperandTextElement, currentOperandTextElement) {
        this.previousOperandTextElement = previousOperandTextElement
        this.currentOperandTextElement = currentOperandTextElement
        this.clear()
    }

    clear() {
        this.currentOperand = ''
        this.previousOperand = ''
        this.operation = undefined
    }

    delete() {
        this.currentOperand = this.currentOperand.toString().slice(0, -1)
    }

    appendNumber(number) {
        if (number === '.' && this.currentOperand.includes('.')) return
        this.currentOperand = this.currentOperand.toString() + number.toString()
    }

    chooseOperation(operation, single = false) {
        this.operation = operation
        if (this.previousOperand !== '' || single) {
            this.compute()
        }
        this.previousOperand = this.currentOperand
        this.currentOperand = ''
    }

    async compute() {
        const prev = parseFloat(this.previousOperand)
        const current = parseFloat(this.currentOperand)

        try {
            const response = await fetch(`/Api/Calculator?numOne=${prev}&numTwo=${current}&operation=${encodeURIComponent(this.operation)}`)
            const result = await response.json();
            console.log(result);
            this.currentOperand = result.success ? result.data.replace(/,/g, '.') : '';
            this.operation = undefined
            this.previousOperand = ''
            await calculator.updateDisplay()

        } catch(e) {
            console.log('failed to compute', e)
        }
    }

    getDisplayNumber(number) {
        const stringNumber = number.toString()
        const integerDigits = parseFloat(stringNumber.split('.')[0])
        const decimalDigits = stringNumber.split('.')[1]
        let integerDisplay
        if (isNaN(integerDigits)) {
            integerDisplay = ''
        } else {
            integerDisplay = integerDigits.toLocaleString('en', {
                maximumFractionDigits: 0
            })
        }
        if (decimalDigits != null) {
            return `${integerDisplay}.${decimalDigits}`
        } else {
            return integerDisplay
        }
    }

    updateDisplay() {
        this.currentOperandTextElement.innerText = this.getDisplayNumber(
            this.currentOperand
        )
        if (this.operation != null) {
            this.previousOperandTextElement.innerText = `${this.getDisplayNumber(
                this.previousOperand
            )} ${this.operation}`
        } else {
            this.previousOperandTextElement.innerText = ''
        }
    }
}

const numberButtons = document.querySelectorAll('[data-number]')
const operationButtons = document.querySelectorAll('[data-operation]')
const equalsButton = document.querySelector('[data-equals]')
const deleteButton = document.querySelector('[data-delete]')
const allClearButton = document.querySelector('[data-all-clear]')
const previousOperandTextElement = document.querySelector(
    '[data-previous-operand]'
)
const currentOperandTextElement = document.querySelector(
    '[data-current-operand]'
)

const calculator = new Calculator(
    previousOperandTextElement,
    currentOperandTextElement
)

numberButtons.forEach((button) => {
    button.addEventListener('click', () => {
        calculator.appendNumber(button.innerText)
        calculator.updateDisplay()
    })
})

operationButtons.forEach((button) => {
    button.addEventListener('click', () => {
        calculator.chooseOperation(button.innerText, button.dataset.single)
    })
})

equalsButton.addEventListener('click', async (button) => {
    await calculator.compute()
    await calculator.updateDisplay()
})

allClearButton.addEventListener('click', (button) => {
    calculator.clear()
    calculator.updateDisplay()
})

deleteButton.addEventListener('click', (button) => {
    calculator.delete()
    calculator.updateDisplay()
})
