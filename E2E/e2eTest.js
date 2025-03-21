import { Selector } from "testcafe";

fixture("Calculator E2E tests (AAA)")
    .page("http://144.24.177.98:3000");

// ADD 
test("Add operation", async t => {
    // ARRANGE
    await t
        .click(Selector('label').withText('Operation:').sibling('select'))
    // ACT
        .click(Selector('option').withText('Add'))
        .typeText(Selector('label').withText('First:').sibling('input'), '5', { replace: true })
        .typeText(Selector('label').withText('Second:').sibling('input'), '3', { replace: true })
        .click(Selector('button').withText('Calculate'))
    // ASSERT
        .expect(Selector('.result-display h2').innerText).eql('Result: 8');
});

// SUBTRACT 
test("Subtract operation", async t => {
    // ARRANGE
    await t
        .click(Selector('label').withText('Operation:').sibling('select'))
    // ACT
        .click(Selector('option').withText('Subtract'))
        .typeText(Selector('label').withText('First:').sibling('input'), '9', { replace: true })
        .typeText(Selector('label').withText('Second:').sibling('input'), '4', { replace: true })
        .click(Selector('button').withText('Calculate'))
    // ASSERT
        .expect(Selector('.result-display h2').innerText).eql('Result: 5');
});

// MULTIPLY 
test("Multiply operation", async t => {
    // ARRANGE
    await t
        .click(Selector('label').withText('Operation:').sibling('select'))
    // ACT
        .click(Selector('option').withText('Multiply'))
        .typeText(Selector('label').withText('First:').sibling('input'), '3', { replace: true })
        .typeText(Selector('label').withText('Second:').sibling('input'), '7', { replace: true })
        .click(Selector('button').withText('Calculate'))
    // ASSERT
        .expect(Selector('.result-display h2').innerText).eql('Result: 21');
});

// DIVIDE 
test("Divide operation", async t => {
    // ARRANGE
    await t
        .click(Selector('label').withText('Operation:').sibling('select'))
    // ACT
        .click(Selector('option').withText('Divide'))
        .typeText(Selector('label').withText('First:').sibling('input'), '8', { replace: true })
        .typeText(Selector('label').withText('Second:').sibling('input'), '2', { replace: true })
        .click(Selector('button').withText('Calculate'))
    // ASSERT
        .expect(Selector('.result-display h2').innerText).eql('Result: 4');
});

// FACTORIAL 
test("Factorial operation", async t => {
    // ARRANGE
    await t
        .click(Selector('label').withText('Operation:').sibling('select'))
    // ACT
        .click(Selector('option').withText('Factorial'))
        .typeText(Selector('label').withText('First:').sibling('input'), '5', { replace: true })
        .click(Selector('button').withText('Calculate'))
    // ASSERT
        .expect(Selector('.result-display h2').innerText).eql('Result: 120');
});

// IS PRIME 
test("Is Prime operation", async t => {
    // ARRANGE
    await t
        .click(Selector('label').withText('Operation:').sibling('select'))
    // ACT
        .click(Selector('option').withText('Is Prime'))
        .typeText(Selector('label').withText('First:').sibling('input'), '7', { replace: true })
        .click(Selector('button').withText('Calculate'))
    // ASSERT
        .expect(Selector('.result-display h2').innerText).eql('Result: true');
});
